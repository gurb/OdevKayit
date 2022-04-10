<script>
import GET from "./FetchLib.vue";
import POST from "./FetchLib.vue";

export default
{
    name: "OdevOlustur",
    mixins: [GET, POST],

    data() {
        return {
            odevler : [],
            postResult: null,
            counter : 0,
        };
    },

    methods: {

        async odevOlustur(){
            this.odevler = this.GET("https://localhost:44358/api/odev", this.odevler);
            this.odevler = await Promise.resolve(this.odevler);

            this.counter = this.counter + 1;
            var index = 0;
            if(this.odevler.length != 0){
                var parse = this.odevler[this.odevler.length - 1];
                index = parse.id;
            }

            // console.log(this.counter);
            console.log(parse);
            const Odev = {
                id: index + 1,
                baslik: this.$refs.odev_baslik.value,
                icerik: this.$refs.odev_icerik.value,
                baslangic: this.$refs.odev_baslangic.value,
                bitis: this.$refs.odev_bitis.value,
            }
            
            console.log(Odev);

            await this.POST('https://localhost:44358/api/odev/', Odev);

        }
    },
    async created() {
        this.odevler = this.GET("https://localhost:44358/api/odev", this.odevler);
        this.odevler = await Promise.resolve(this.odevler);
    },
    async mounted()
    {
        this.odevler = this.GET("https://localhost:44358/api/odev", this.odevler);
        this.odevler = await Promise.resolve(this.odevler);
    },
};
</script>

<template>
    <div>
        <h3>Ödev Ekle</h3>

        <form @submit.prevent="odevOlustur">
            <div class="mb-3">
                <label for="Baslik">Başlık Giriniz</label>
                <input type="text" class="form-control" ref="odev_baslik">
            </div>
            <div class="mb-3">
                <label for="Icerik">İçerik Giriniz</label>
                <input type="text" class="form-control" ref="odev_icerik">
            </div>
            <div class="mb-3">
                <label for="Baslangic">Başlangıç Tarihi</label>
                <input id="startDate" class="form-control" ref="odev_baslangic"  type="datetime-local" />
            </div>
            <div class="mb-3">
                <label for="Bitis">Bitiş Tarihi</label>
                <input id="endDate" class="form-control" ref="odev_bitis"  type="datetime-local" />
            </div>
            <button type="submit" class="btn btn-primary">Ekle</button>
        </form>

    </div>
</template>


<style scoped>
th{
    font-weight: bold;
}
</style>
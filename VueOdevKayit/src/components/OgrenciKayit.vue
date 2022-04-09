<script>

import GET from "./FetchLib.vue";
import POST from "./FetchLib.vue";
import GETID from "./FetchLib.vue";

export default
{
    name: "OgrenciKayit",
    mixins: [GET, POST, GETID],
    data() {
        return {
            ogrenciler : [],
            postResult: null,
            counter : 0,
            odevler : [],
        };
    },

    methods: {
        async ogrenciOlustur(){    
            this.ogrenciler = this.GET("https://localhost:44358/api/ogrenci", this.ogrenciler);
            this.ogrenciler = await Promise.resolve(this.ogrenciler);

            this.counter = this.counter + 1;
            var index = 0;

            if(this.ogrenciler.length != 0){
                var parse = this.ogrenciler[this.ogrenciler.length - 1];
                index = parse.id;
            }

            // console.log(this.counter);
            console.log(parse);
            const Ogrenci = {
                id: index + 1,
                isim: this.$refs.ogrenci_isim.value,
                soyisim: this.$refs.ogrenci_soyisim.value,
                odevid: parseInt(this.$refs.ogrenci_odev.value),
                odev: null,
            }

            Ogrenci.odev = this.GETID("https://localhost:44358/api/odev/", this.ogrenciler, Ogrenci.odevid);
           
            console.log(Ogrenci);
           
            this.POST('https://localhost:44358/api/ogrenci/', Ogrenci);
        },

        async odevAta(){
            const requestOptions = {
                method: 'PUT',
                headers: {
                    // 'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Access-Control-Allow-Origin' : "*", 
                    'Access-Control-Allow-Credentials' : true 
                },
                mode: "cors",
                body: JSON.stringify( Ogrenci ), 
            };
        }
    },
    async created() {
        this.ogrenciler = this.GET("https://localhost:44358/api/ogrenci", this.ogrenciler);
        this.ogrenciler = await Promise.resolve(this.ogrenciler);

        this.odevler = this.GET("https://localhost:44358/api/odev", this.odevler);
        this.odevler = await Promise.resolve(this.odevler);
    },
    async mounted()
    {
        this.ogrenciler = this.GET("https://localhost:44358/api/ogrenci", this.ogrenciler);
        this.ogrenciler = await Promise.resolve(this.ogrenciler);

        this.odevler = this.GET("https://localhost:44358/api/odev", this.odevler);
        this.odevler = await Promise.resolve(this.odevler);
    },
};
</script>

<template>
    <div>
        <h3>Öğrenci Kayıt</h3>

        <!-- <form @submit.prevent="odevOlustur">
            <div>
                <input type="text" id="Baslik" ref="odev_baslik">
            </div>
            <div>
                <label for="Icerik">İçerik Giriniz</label>
                <input type="text" id="Icerik" ref="odev_icerik">
            </div>
            <div>
                <label for="Baslangic">Başlangıç Tarihi</label>
                <input id="startDate" class="form-control" ref="odev_baslangic"  type="datetime-local" />

            </div>
            <div>
                <label for="Bitis">Bitiş Tarihi</label>
                <input id="startDate" class="form-control" ref="odev_bitis"  type="datetime-local" />
            </div>
            <button type="submit">Ekle</button>
        </form> -->

        <form @submit.prevent="ogrenciOlustur">
            <div class="mb-3">
                <label for="Isim">İsim</label>
                <input type="text" class="form-control" ref="ogrenci_isim">
            </div>
            <div class="mb-3">
                <label for="Soyisim">Soyisim</label>
                <input type="text" class="form-control"  ref="ogrenci_soyisim">
            </div>
            <div class="mb-3">
                <label for="Odev">Odev Ata</label>
                <select class="form-select" aria-label="Default select example" ref="ogrenci_odev" v-model="odev">
                    <option :value="null" disabled>-- Please select an option --</option>
                    <option v-for="odev in odevler" :value="odev.id" >
                        {{ odev.baslik }}
                    </option>
                </select>
            </div>
            <button type="submit" class="btn btn-primary">Kaydet</button>
        </form>

    </div>
</template>


<style scoped>
th{
    font-weight: bold;
}
</style>
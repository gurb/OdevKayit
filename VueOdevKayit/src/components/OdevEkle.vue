<script>
export default
{
    name: "OdevOlustur",

    data() {
        return {
            odevler : [],
            postResult: null,
            counter : 0,
        };
    },

    methods: {
        async getData() {
            const requestOptions = {
                method: 'GET'
            };   
            try {
                fetch("https://localhost:44358/api/odev", requestOptions)
                    .then(response => response.json())
                    .then(json => this.odevler = json)
            } catch (err) {
                console.log(err);
            }
        },

        async odevOlustur(){
            this.getData();
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
                bitis: new Date(),
            }
            console.log(Odev);
            const requestOptions = {
                method: 'POST',
                headers: {
                    // 'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Access-Control-Allow-Origin' : "*", 
                    'Access-Control-Allow-Credentials' : true 
                },
                mode: "cors",
                body: JSON.stringify( Odev ), 
            };
            await fetch('https://localhost:44358/api/odev/', requestOptions)
                .then(response => { response.json(); this.getData(); })
                .then(data => this.formData = data)
                .catch((err) => {
                    console.log(err);
                });
        }
    },
    created() {
        this.getData();
    },
    mounted()
    {
        this.getData();
    },
};
</script>

<template>
    <div>
        <h1>Ödev Ekle</h1>

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

        <form @submit.prevent="odevOlustur">
            <div class="mb-3">
                <label for="Baslik">Başlık Giriniz</label>
                <input type="text" class="form-control" id="exampleInputEmail1" aria-describedby="emailHelp" ref="odev_baslik">
            </div>
            <div class="mb-3">
                <label for="Icerik">İçerik Giriniz</label>
                <input type="text" class="form-control" id="exampleInputPassword1" ref="odev_icerik">
            </div>
            <div class="mb-3">
                <label for="Baslangic">Başlangıç Tarihi</label>
                <input id="startDate" class="form-control" ref="odev_baslangic"  type="datetime-local" />
            </div>
            <div class="mb-3">
                <label for="Bitis">Bitiş Tarihi</label>
                <input id="endDate" class="form-control" ref="odev_bitis"  type="datetime-local" />
            </div>
            <button type="submit" class="btn btn-primary">Submit</button>
        </form>

    </div>
</template>


<style scoped>
th{
    font-weight: bold;
}
</style>
<script>
export default
{
    name: "OgrenciKayit",

    data() {
        return {
            ogrenciler : [],
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
                fetch("https://localhost:44358/api/ogrenci", requestOptions)
                    .then(response => response.json())
                    .then(json => this.ogrenciler = json)
            } catch (err) {
                console.log(err);
            }
        },

        async ogrenciOlustur(){
            this.getData();
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
            }
            console.log(Ogrenci);
            const requestOptions = {
                method: 'POST',
                headers: {
                    // 'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Access-Control-Allow-Origin' : "*", 
                    'Access-Control-Allow-Credentials' : true 
                },
                mode: "cors",
                body: JSON.stringify( Ogrenci ), 
            };
            await fetch('https://localhost:44358/api/ogrenci/', requestOptions)
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
        <h1>Öğrenci Kayıt</h1>

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
                <input type="text" class="form-control" id="exampleInputPassword1" ref="ogrenci_odev">
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
<script>
export default
{
    name: "OdevOlustur",

    data() {
        return {
            postResult: null
        };
    },

    methods: {
        async odevOlustur(){
            const odevData = {
                Baslik: this.$refs.odev_baslik,
                Icerik: this.$refs.odev_icerik,
                Baslangic: this.$refs.odev_baslangic,
                Bitis: this.$refs.odev_bitis,
            }
            const requestOptions = {
                method: 'POST',
                Headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify( odevData ), 
            };
            fetch('https://localhost:44358/api/odev/', requestOptions)
                .then(response => response.json())
                .then(data => this.formData = data)
        }
    },

};
</script>

<template>
    <div>
        <h1>Ödev Ekle</h1>

        <form @submit.prevent="odevOlustur">
            <div>
                <label for="Baslik">Başlık Giriniz</label>
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
            <button>Ekle</button>
        </form>

    </div>
</template>
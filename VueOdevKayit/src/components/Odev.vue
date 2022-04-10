<script>
import {ref} from "vue";
import Modal from './Modal.vue';
import GET from "./FetchLib.vue";
import DELETE from "./FetchLib.vue";
import PUT from "./FetchLib.vue";
import { checkCompatEnabled } from "@vue/compiler-core";
import { isProxy, toRaw } from 'vue';

export default
{
    // -- for modal
    components: { Modal },
    setup () {
        const isOpen = ref(false);
        const isUpdate = ref(false);

        return { isOpen, isUpdate }
    },
    // -- end for modal

    mixins: [GET, DELETE, PUT],

    data() {
        return {
            id : 0,
            odevler: [],
            seciliOdev: null,
            ogrenciler: [],
            seciliOgrenciler: [],
        };
    },
    methods: {
        async odevSil(event, id) {
            this.getOgrenci();
            this.odevler = await this.DELETE("https://localhost:44358/api/odev/", id, this.odevler);
            this.odevler = await Promise.resolve(this.odevler);
        },
        async ogrenciSil(event, id){
            this.ogrenciler = await this.DELETE("https://localhost:44358/api/ogrenci/", id, this.ogrenciler);
            this.ogrenciler = await Promise.resolve(this.ogrenciler);
        },
        async odevGuncelle(){
            if(this.seciliOdev != null){
                this.seciliOdev.baslik = this.$refs.odev_baslik.value;
                this.seciliOdev.icerik = this.$refs.odev_icerik.value;
                this.seciliOdev.baslangic = this.$refs.odev_baslangic.value;
                this.seciliOdev.bitis = this.$refs.odev_bitis.value;

                await this.PUT("https://localhost:44358/api/odev/", this.seciliOdev, this.seciliOdev.id);
                this.refreshData();
            }
        },
        async detayAc(event, odev)
        {
            isOpen = true;
            seciliOdev = odev;
            this.getOgrenci();
        },
        async refreshData(){
            this.getOdev();
            this.getOgrenci();
        },
        async getOdev()
        {
            this.odevler = this.GET("https://localhost:44358/api/odev", this.odevler);
            this.odevler = await Promise.resolve(this.odevler);
        },
        async getOgrenci()
        {
            this.ogrenciler = this.GET("https://localhost:44358/api/ogrenci/", this.ogrenciler);
            this.ogrenciler = await Promise.resolve(this.ogrenciler);
        },
        /** belli ödeve ait olan öğrenci listesi */
        async ogrenciListe(odevid)
        {
            this.seciliOgrenciler = this.GET("https://localhost:44358/api/ogrenci/listele/" + odevid, this.seciliOgrenciler);
            this.seciliOgrenciler = await Promise.resolve(this.seciliOgrenciler);
        }
    },
    async created() {
        this.odevler = this.GET("https://localhost:44358/api/odev", this.odevler);
        this.odevler = await Promise.resolve(this.odevler);
        this.getOgrenci();
        console.log(this.odevler);
    },
    async mounted() {
        this.odevler = this.GET("https://localhost:44358/api/odev", this.odevler);
        this.getOgrenci();

        console.log(this.odevler);
    },
    components: { Modal }
};
</script>

<template>
    <div>
        <h3>Ödevler</h3>
        <table class="table" ref="table">
            <thead>
                <tr>
                    <th scope="col">#</th>
                    <th scope="col">Başlık</th>
                    <th scope="col">İçerik</th>
                    <th scope="col">Baslangıç Tarihi</th>
                    <th scope="col">Bitiş Tarihi</th>
                    <th scope="col">İşlemler</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="odev in odevler" v-bind:key="odev.id"> 
                    <th scope="row">{{ odev.id }}</th>
                    <td>
                        <a class="odev-link" @click="isOpen = true, seciliOdev = odev, ogrenciListe(odev.id)"> {{ odev.baslik }} </a>
                    </td>
                    <td>{{ odev.icerik }}</td>
                    <td>{{ odev.baslangic }}</td>
                    <td>{{ odev.bitis }}</td>
                    <td>
                        <button @click="isUpdate = true, seciliOdev = odev" class="action-button"><i class="bi bi-pencil"></i></button>
                        <button @click="odevSil($event, odev.id)"><i class="bi bi-trash"></i></button>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    

    <!-- for modal odev detay -->
    <!-- <button @click="isOpen = true">show modal</button> -->
    <Modal :open="isOpen" @close="isOpen = !isOpen">
        <div v-if="seciliOdev"> 
            <h5> {{ seciliOdev.baslik }}</h5>
            <div class="date-area">
                <b>Başlangıç Tarihi:</b> {{ seciliOdev.baslangic }}
                <br/>
                <b>Bitiş Tarihi:</b> {{ seciliOdev.bitis }}
            </div>
            <h6>İçerik: </h6>
            <p class="odev-icerigi">{{ seciliOdev.icerik }}</p>
            <h6>Öğrenciler</h6>
            <ul class="ogrenci-listesi" v-for="secili in seciliOgrenciler" v-bind:key="secili.id">
                <li> {{ secili.isim + " " + secili.soyisim }} </li>
            </ul>
        </div>
    </Modal>
    <!-- for modal -->

    <!-- odev update form -->
    <Modal :open="isUpdate" @close="isUpdate = !isUpdate">
        <div v-if="seciliOdev">
            <form @submit.prevent="odevGuncelle">
                <div class="mb-3">
                    <label for="Baslik">Başlık Giriniz</label>
                    <input type="text" class="form-control" ref="odev_baslik" :value="seciliOdev.baslik">
                </div>
                <div class="mb-3">
                    <label for="Icerik">İçerik Giriniz</label>
                    <input type="text" class="form-control" ref="odev_icerik" :value="seciliOdev.icerik">
                </div>
                <div class="mb-3">
                    <label for="Baslangic">Başlangıç Tarihi</label>
                    <input id="startDate" class="form-control" ref="odev_baslangic"  type="datetime-local" :value="seciliOdev.baslangic"/>
                </div>
                <div class="mb-3">
                    <label for="Bitis">Bitiş Tarihi</label>
                    <input id="endDate" class="form-control" ref="odev_bitis"  type="datetime-local" :value="seciliOdev.bitis"/>
                </div>
            <button type="submit" class="btn btn-primary">Güncelle</button>
        </form>
        </div>
    </Modal>
    <!-- odev update form -->

</template>

<style scoped>
th{
    font-weight: bold;
}
.odev-link{
    font-weight: 600;
    color: #0985f8;
    text-decoration: underline;
    cursor: pointer;
}
.odev-icerigi {
    font-size: 13px;
}
.date-area {
    font-size: 13px;
    margin: 20px 0;
}

.ogrenci-listesi{
    padding:0;
    margin:0;
}

.ogrenci-listesi li{
    list-style-type: none;
    font-size: 13px;
    border-bottom: 1px solid rgb(197, 197, 197);
}

.ogrenci-listesi li:last-child
{
    border: none;
}
.action-button {
    margin-right: 5px;
}
</style>
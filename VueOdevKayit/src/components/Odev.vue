<script>
import {ref} from "vue";
import Modal from './Modal.vue';
import GET from "./FetchLib.vue";
import DELETE from "./FetchLib.vue";

export default
{
    // -- for modal
    components: { Modal },
    setup () {
        const isOpen = ref(false);

        return { isOpen }
    },
    // -- end for modal
    mixins: [GET, DELETE],

    data() {
        return {
            id : 0,
            odevler: [],
            seciliOdev: null,
            ogrenciler: [],
        };
    },
    methods: {
        async odevSil(event, id) {
            this.odevler = await this.DELETE("https://localhost:44358/api/odev/", id, this.odevler);
            this.odevler = await Promise.resolve(this.odevler);
        },
        async detayAc(event, odev)
        {
            isOpen = true;
            seciliOdev = odev;
        }
    },
    async created() {
        this.odevler = this.GET("https://localhost:44358/api/odev", this.odevler);
        this.odevler = await Promise.resolve(this.odevler);
        
        console.log(this.odevler);
    },
    async mounted() {
        this.odevler = this.GET("https://localhost:44358/api/odev", this.odevler);
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
                    <th scope="col">Sil</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="odev in odevler" v-bind:key="odev.id"> 
                    <th scope="row">{{ odev.id }}</th>
                    <td>
                        <a class="odev-link" @click="isOpen = true, seciliOdev = odev"> {{ odev.baslik }} </a>
                    </td>
                    <td>{{ odev.icerik }}</td>
                    <td>{{ odev.baslangic }}</td>
                    <td>{{ odev.bitis }}</td>
                    <td><button @click="odevSil($event, odev.id)"><i class="bi bi-trash"></i></button></td>
                </tr>
            </tbody>
        </table>
    </div>
    

    <!-- for modal -->
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
            <ul v-for="ogrenci in ogrenciler" v-bind:key="ogrenci.id">
                <li> {{ ogrenci.isim, ogrenci.soyisim}} </li>
            </ul>
        </div>
    </Modal>
    <!-- for modal -->
</template>


<style scoped>
th{
    font-weight: bold;
}
.odev-link{
    font-weight: 600;
    color: #0985f8;
    text-decoration: underline;
}
.odev-icerigi {
    font-size: 13px;
}
.date-area {
    font-size: 13px;
    margin: 20px 0;
}
</style>
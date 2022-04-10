<script>
import {ref} from "vue";
import Modal from './Modal.vue';
import GET from "./FetchLib.vue";
import GETID from "./FetchLib.vue";
import POST from "./FetchLib.vue";
import DELETE from "./FetchLib.vue";
import PUT from "./FetchLib.vue";

export default
{
    // -- for modal
    components: { Modal },
    setup () {
        const isOpen = ref(false);

        return { isOpen }
    },
    // -- end for modal

    mixins: [GET, POST, DELETE, GETID, PUT],

    data() {
        return {
            ogrenciler : [],
            seciliOgrenci: null,
            seciliOgrenciOdev: null,
            odevler: [],
        };
    },

    methods: {
        async ogrenciSil(event, id){
            this.ogrenciler = await this.DELETE("https://localhost:44358/api/ogrenci/", id, this.ogrenciler);
            this.ogrenciler = await Promise.resolve(this.ogrenciler);
        },
        async ogrenciGuncelle(){
            if(this.seciliOgrenci != null){
                this.seciliOgrenci.isim = this.$refs.ogrenci_isim.value;
                this.seciliOgrenci.soyisim = this.$refs.ogrenci_soyisim.value;
                this.seciliOgrenci.odevid = parseInt(this.$refs.ogrenci_odev.value);
                this.seciliOgrenciOdev = this.GETID("https://localhost:44358/api/odev/", this.seciliOgrenciOdev, this.seciliOgrenci.odevid);
                await this.PUT("https://localhost:44358/api/ogrenci/", this.seciliOgrenci, this.seciliOgrenci.id);
                this.refreshData();
            } 
        },
        async setSeciliOgrenciOdev(id)
        {
            this.seciliOgrenciOdev = this.GETID("https://localhost:44358/api/odev/", this.seciliOgrenciOdev, id);
        },
        async refreshData(){
            this.ogrenciler = this.GET("https://localhost:44358/api/ogrenci", this.ogrenciler);
            this.ogrenciler = await Promise.resolve(this.ogrenciler);

            this.odevler = this.GET("https://localhost:44358/api/odev", this.odevler);
            this.odevler = await Promise.resolve(this.odevler);
        }
    },

    async created() {
        this.ogrenciler = this.GET("https://localhost:44358/api/ogrenci", this.ogrenciler);
        this.ogrenciler = await Promise.resolve(this.ogrenciler);

        this.odevler = this.GET("https://localhost:44358/api/odev", this.odevler);
        this.odevler = await Promise.resolve(this.odevler);
    },
    async mounted(){
        this.ogrenciler = this.GET("https://localhost:44358/api/ogrenci", this.ogrenciler);
        this.ogrenciler = await Promise.resolve(this.ogrenciler);

        this.odevler = this.GET("https://localhost:44358/api/odev", this.odevler);
        this.odevler = await Promise.resolve(this.odevler);
    },
};
</script>

<template>
    <div>
        <h3>Öğrenciler</h3>
        <table class="table" ref="table">
            <thead>
                <tr>
                    <th scope="col">#</th>
                    <th scope="col">İsim</th>
                    <th scope="col">Soyisim</th>
                    <!-- <th scope="col">Ödev ID</th> -->
                    <th scope="col">İşlemler</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="ogrenci in ogrenciler" v-bind:key="ogrenci.id"> 
                    <th scope="row">{{ ogrenci.id }}</th>
                    <td>{{ ogrenci.isim }}</td>
                    <td>{{ ogrenci.soyisim }}</td>
                    <!-- <td>{{ ogrenci.odevId }}</td> -->
                    <td>
                        <button @click="isOpen = true, seciliOgrenci = ogrenci, setSeciliOgrenciOdev(parseInt(ogrenci.odevid))" class="action-button"><i class="bi bi-pencil"></i></button>
                        <button class="action-button" @click="ogrenciSil($event, ogrenci.id)"><i class="bi bi-trash"></i></button>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>

    <Modal :open="isOpen" @close="isOpen = !isOpen">
        <div v-if="seciliOgrenci">
            <form @submit.prevent="ogrenciGuncelle">
                <div class="mb-3">
                    <label for="Isim">İsim</label>
                    <input type="text" class="form-control" ref="ogrenci_isim" :value="seciliOgrenci.isim">
                </div>
                <div class="mb-3">
                    <label for="Soyisim">Soyisim</label>
                    <input type="text" class="form-control"  ref="ogrenci_soyisim" :value="seciliOgrenci.soyisim">
                </div>
                <div class="mb-3">
                    <label for="Odev">Ödev Ata</label>
                    <select class="form-select" aria-label="Default select example" ref="ogrenci_odev" v-model="odev">
                        <option v-if="seciliOgrenciOdev" :value="seciliOgrenciOdev.id">{{ seciliOgrenciOdev.baslik }}</option>
                        <option :value="null" disabled v-else>-- Aşağıdaki Ödevlerden Birini Seçin --</option>
                        <option v-for="odev in odevler" :value="odev.id" >
                            {{ odev.baslik }}
                        </option>
                    </select>
                </div>
                <button type="submit" class="btn btn-primary">Güncelle</button>
            </form> 
        </div>
    </Modal>
</template>


<style scoped>
th{
    font-weight: bold;
}
.action-button {
    margin-right: 5px;
}
</style>
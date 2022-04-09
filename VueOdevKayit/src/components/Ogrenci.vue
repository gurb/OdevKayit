<script>
import GET from "./FetchLib.vue";
import POST from "./FetchLib.vue";
import DELETE from "./FetchLib.vue";

export default
{
    mixins: [GET, POST, DELETE],

    data() {
        return {
            ogrenciler : [],
        };
    },

    methods: {
        async ogrenciSil(event, id){
            this.ogrenciler = await this.DELETE("https://localhost:44358/api/ogrenci/", id, this.ogrenciler);
            this.ogrenciler = await Promise.resolve(this.ogrenciler);
        },
    },

    async created() {
        this.ogrenciler = this.GET("https://localhost:44358/api/ogrenci", this.ogrenciler);
        this.ogrenciler = await Promise.resolve(this.ogrenciler);
    },
    async mounted(){
        this.ogrenciler = this.GET("https://localhost:44358/api/ogrenci", this.ogrenciler);
        this.ogrenciler = await Promise.resolve(this.ogrenciler);
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
                    <th scope="col">Sil</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="ogrenci in ogrenciler" v-bind:key="ogrenci.id"> 
                    <th scope="row">{{ ogrenci.id }}</th>
                    <td>{{ ogrenci.isim }}</td>
                    <td>{{ ogrenci.soyisim }}</td>
                    <!-- <td>{{ ogrenci.odevId }}</td> -->
                    <td>
                        <button class="action-button"><i class="bi bi-pencil"></i></button>
                        <button class="action-button" @click="ogrenciSil($event, ogrenci.id)"><i class="bi bi-trash"></i></button>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</template>


<style scoped>
th{
    font-weight: bold;
}
.action-button {
    margin-right: 5px;
}
</style>
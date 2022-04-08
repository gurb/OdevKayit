<script>
export default
{
    data() {
        return {
            ogrenciler : [],
        };
    },

    methods: {
        async getData() {
            const requestOptions = {
                method: 'GET'
            };   
            try {
                await fetch("https://localhost:44358/api/ogrenci", requestOptions)
                    .then(response => response.json())
                    .then(json => this.ogrenciler = json)
            } catch (err) {
                console.log(err);
            }
        },
        async ogrenciSil(event, id){
            const requestOptions = {
                method: 'DELETE'
            };   
            try {
                await fetch("https://localhost:44358/api/ogrenci/" + id, requestOptions)
                    .then(response => { response.json(); this.getData(); })
                    // .then(json => this.odevler = json)
            } catch (err) {
                console.log(err);
            }
            
        },
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
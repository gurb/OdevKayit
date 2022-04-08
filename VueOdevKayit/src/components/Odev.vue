<script>
export default
{
    data() {
        return {
            odevler : [],
        };
    },

    methods: {
        async getData() {
            const requestOptions = {
                method: 'GET'
            };   
            try {
                await fetch("https://localhost:44358/api/odev", requestOptions)
                    .then(response => response.json())
                    .then(json => this.odevler = json)
            } catch (err) {
                console.log(err);
            }
        },
    },

    created() {
        this.getData();
    },
};
</script>

<template>
    <div>
        <h1>Ödevler</h1>
        <table class="table">
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
                    <td>{{ odev.baslik }}</td>
                    <td>{{ odev.icerik }}</td>
                    <td>{{ odev.baslangic }}</td>
                    <td>{{ odev.bitis }}</td>
                    <td><i class="bi bi-trash"></i></td>
                </tr>
            </tbody>
        </table>
    </div>
</template>


<style scoped>
th{
    font-weight: bold;
}
</style>
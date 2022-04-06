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

        <ol v-for="odev in odevler" v-bind:key="odev.id">
            <li>{{ odev.baslik }}</li>
            <p>{{ odev.icerik }}</p>
        </OL>
    </div>
</template>
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
            try {
                let response = await fetch("https://localhost:44358/api/odev");
                this.odevler = await response.json();
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

        <ul v-for="odev in odevler" v-bind:key="od.id">
            <li>{{ odev.Baslik }}</li>
            <p>{{ odev.Icerik }}</p>
        </ul>
    </div>
</template>
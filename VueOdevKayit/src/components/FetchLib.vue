<script>
export default {
    methods: {
        async GET(apiPath, data) {
            const requestOptions = {
                method: 'GET'
            };   
            try {
                await fetch(apiPath, requestOptions)
                    .then(response => response.json())
                    .then(json => data = json)
            } catch (err) {
                console.log(err);
            }

            return data;
        },

        async GETID(apiPath, data, id) {
            const requestOptions = {
                method: 'GET'
            };   
            try {
                await fetch(apiPath + id, requestOptions)
                    .then(response => response.json())
                    .then(json => data = json)
            } catch (err) {
                console.log(err);
            }

            return await Promise.resolve(data);
        },
        
        async POST(apiPath, data) {
            const requestOptions = {
                method: 'POST',
                headers: {
                    // 'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Access-Control-Allow-Origin' : "*", 
                    'Access-Control-Allow-Credentials' : true 
                },
                mode: "cors",
                body: JSON.stringify( data ), 
            };   
            try {
                await fetch(apiPath, requestOptions)
                    .then(response => { response.json(); })
                    .then(data => this.formData = data)
                    .catch((err) => {
                        console.log(err);
                    });
            } catch (err) {
                console.log(err);
            }
        },

        async DELETE(apiPath, id, data) {
            const requestOptions = {
                method: 'DELETE',
                headers: {
                    // 'Accept': 'application/json',
                    'Access-Control-Allow-Origin' : "*", 
                    'Access-Control-Allow-Credentials' : true 
                },
            };   
            try {
                await fetch(apiPath + id, requestOptions)
                    .then(response => { 
                        response.json();
                    })
                    // .then(json => this.odevler = json)
            } catch (err) {
                console.log(err);
            }
            return this.GET(apiPath, data);
        },

        async PUT(apiPath, id, data) {
            const requestOptions = {
                method: 'PUT',
                headers: {
                    // 'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Access-Control-Allow-Origin' : "*", 
                    'Access-Control-Allow-Credentials' : true 
                },
                mode: "cors",
                body: JSON.stringify( data ), 
            };
            try {
                await fetch(apiPath + id, requestOptions)
                    .then(response => { response.json(); })
                    .then(data => this.formData = data)
                    .catch((err) => {
                        console.log(err);
                    });
            } catch (err) {
                console.log(err);
            }
        },
    }
}
</script>
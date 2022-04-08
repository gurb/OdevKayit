<template>
    <div class="vue-modal" v-show="open">
        <div class="vue-modal-inner">
            <div class="vue-modal-content">
                <slot/>
                <button type="button" class="close close-button" aria-label="Close"  @click="$emit('close')">
                    <span aria-hidden="true">&times;</span>
                </button>
                
            </div>
        </div>
    </div>
</template>

<script>
export default {
    props: {
        open: {
            type: Boolean,
            required: true
        }
    }
}
</script>

<style scoped>
*,
::before,
::after {
    margin: 0;
    padding: 0;
    box-sizing: border-box;
}

.vue-modal {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    overflow-x: hidden;
    overflow-y: auto;
    background-color: rgba(0,0,0,0.5);
    z-index: 1;
}

.vue-modal-inner {
    max-width: 600px;
    margin: auto;
    margin-top:200px;
}

.vue-modal-content {
    position: relative;
    background-color: #fff;
    border: 2px solid rgb(0, 0, 0);
    border-radius: 5px;
    background-clip: padding-box;
    padding: 20px;
}

.close-button {
    position: absolute;
    top: 10px;
    right: 20px;
    border:none;
    background-color: #fff;
    
}
</style>
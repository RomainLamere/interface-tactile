<template>
    <div class="color-marker" :style="`left: calc(${position}px - 3px); background-color: ${pickedColor}`">
        <input type="color" class="color-picker" v-model="pickedColor">
    </div>
</template>
<script>
export default {
    data() {
        return {
            pickedColor: '#000000'
        }
    },
    props:{
        position: Number
    }
}
</script>
<style lang="css" scoped>
    .color-marker{
        cursor: pointer;
        position: absolute;
        top: 0;
        height: 50px;
        width: 5px;
        background-color: white;
    }

    .color-marker .color-picker{
        cursor: pointer;
        width: 3px;
        padding: 0;
        height: inherit;
        opacity: 0;
    }
</style>
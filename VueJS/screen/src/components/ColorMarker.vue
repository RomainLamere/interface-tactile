<template>
    <div class="color-marker" :style="`left: calc(${marker.position}px - 3px); background-color: ${pickedColor}`">
        <input type="color" class="color-picker" v-model="pickedColor">
    </div>
</template>
<script>
export default {
    data() {
        return {
            pickedColor: '#ffffff'
        }
    },
    props:{
        marker: {position: Number, color: String}
    },
    watch: {
        pickedColor: function(val){
            this.pickedColor = val;
            this.$emit('changedcolor',{position: this.marker.position, color:this.pickedColor});
        }
    },
    created() {
        this.pickedColor = this.marker.color;
    },
}
</script>
<style lang="css" scoped>
    .color-marker{
        cursor: pointer;
        position: absolute;
        top: 0;
        height: 70px;
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
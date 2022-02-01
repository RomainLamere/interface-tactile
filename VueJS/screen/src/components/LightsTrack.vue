<template>
  <div class="color-track" id="color-track" @click="clickEvent">
    <input type="color" name="color" id="color" v-model="color" />
    {{ color }}
    <button @click="sendColor()">Send new color</button>
    <ColorMarker v-for="colorMarker in colorMarkers" :key="colorMarker.position" :position="colorMarker.position"></ColorMarker>
  </div>
</template>
<script>
import ColorMarker from './ColorMarker.vue';
export default {
    components:{
        ColorMarker
    },
    data() {
        return {
            color: "#000000",
        colorMarkers: []
    };
  },
  sockets: {},
  methods: {
    sendColor() {
      this.$socket.emit("changeLights", this.color);
    },
    clickEvent(event){
        const rect = event.target.getBoundingClientRect();
        const x = event.clientX - rect.left; //x position within the element.
        console.log("Left? : " + x);
        if(!(this.colorMarkers.find(c => (c.position - 3 === x || c.position === x || c.position + 3 === x))
            || event.target.className !== 'color-track')){
            this.colorMarkers.push({position: x});
        }
    },
  }
};
</script>
<style lang="css" scoped>
    .color-track{
        position: relative;
        width: 100%;
        height: 50px;
        background-color: gray;
    }
</style>
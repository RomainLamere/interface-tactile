<template>
  <div class="color-track" id="color-track" @click="clickEvent">
    <input type="color" name="color" id="color" v-model="color" />
    {{ color }}
    <button @click="sendColor()">Send new color</button>
    <button @click="bouleDisco()">Boule disco</button>
    <ColorMarker
      v-for="colorMarker in colorMarkers"
      :key="colorMarker.position"
      :position="colorMarker.position"
      v-on:changedcolor="markerColorChanged"></ColorMarker>
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
            this.colorMarkers.push({position: x, color: '#ffffff'});
        }
    },
    markerColorChanged(event){
      this.colorMarkers.find(c => c.position === event.position).color = event.color;
    },
    bouleDisco(){
      const test = [...this.colorMarkers].sort((a,b)=>a.position-b.position);
      test.forEach(c => {
        setTimeout(()=>{
          this.$socket.emit('changeLights',c.color);
        }, c.position * 10)
      })
      console.log(test);
    }
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
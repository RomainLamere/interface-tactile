<template>
  <div class="lineSound">
    <Track v-for="(n, index) in trackArray" :key="index" v-on:sourceadded="disableTrack()" :bus="bus"></Track>
  </div>
</template>

<script>
import Track from "./Track";
import Vue from "vue";
export default {
  name: "TrackLine",
  props:{
    busCol: Vue
  },
  components: {Track},
  data(){
    return {
      bus: new Vue(),
      bpm: 0,
      trackArray: [0]
    }
  },
  created() {
    this.busCol.$on('playTrack', () => {
      this.playTrack();
    });
  },
  sockets:{},
  methods:{
    disableTrack(){
      if(this.trackArray.length===1){
        this.$emit("addLine");
      }
      this.trackArray.push(this.trackArray[this.trackArray.length-1]+1);
      console.log('Src added in track line');
    },
    playTrack(){
      this.bus.$emit('playTrack', {
      });
    }
  },
}
</script>

<style scoped>
.lineSound{
  display: flex;
}
</style>

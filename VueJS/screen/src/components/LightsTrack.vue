<template>
  <div class="lights-track" :style="`width: ${width}`" ref="lightsTrack" @touchstart="touchStart" @touchend="touchEnd">
    <div class="playButton">
      <img v-if="!colorsTimeouts.length" src="@/assets/icons/bouton-jouer.png" @click="bouleDisco()" alt=""/>
      <img v-else src="@/assets/icons/stop-button.png" @click="stopBouleDisco()" alt=""/>
    </div>
    <div class="color-track" @click="clickEvent">
      <!-- <input type="color" name="color" id="color" v-model="color" />
      {{ color }} -->
      <!-- <button @click="sendColor()">Send new color</button> -->
      <!-- <button @click="bouleDisco()">Start color track</button> -->
      <!-- <button @click="loop()">Start a loop with the color track</button> -->
      <img :src="require(`@/assets/icons/light.png`)">
      <ColorMarker
        v-for="colorMarker in colorMarkers"
        :key="colorMarker.position"
        :marker="colorMarker"
        v-on:changedcolor="markerColorChanged"
      ></ColorMarker>
    </div>
  </div>
</template>
<script>
import { Vue } from 'vue-property-decorator';
import ColorMarker from "./ColorMarker.vue";
export default {
  components: {
    ColorMarker,
  },
  props:{
    busCol: Vue,
    lightId: Number
  },
  data() {
    return {
      width: '100%',
      unwatch: null,
      color: "#000000",
      colorsTimeouts: [],
      colorMarkers: [],
      touchStartX: 0
      //Preset rainbow
      // colorMarkers: [
      //   { position: 45, color: "#ff0000" },
      //   { position: 90, color: "#ffa200" },
      //   { position: 135, color: "#eeff00" },
      //   { position: 180, color: "#1eff00" },
      //   { position: 225, color: "#00ff6e" },
      //   { position: 270, color: "#00ffe1" },
      //   { position: 315, color: "#009dff" },
      //   { position: 360, color: "#004cff" },
      //   { position: 405, color: "#002aff" },
      //   { position: 450, color: "#0011ff" },
      //   { position: 495, color: "#5900ff" },
      //   { position: 550, color: "#9900ff" },
      //   { position: 595, color: "#c800ff" },
      //   { position: 640, color: "#d400ff" },
      //   { position: 685, color: "#ff00d0" },
      //   { position: 730, color: "#ff0066" },
      //   { position: 775, color: "#ff0000" },
      // ],
      //Preset rainbow FAST
      // colorMarkers: [
      //   { position: 10, color: "#ff0000" },
      //   { position: 20, color: "#ffa200" },
      //   { position: 30, color: "#eeff00" },
      //   { position: 40, color: "#1eff00" },
      //   { position: 50, color: "#00ff6e" },
      //   { position: 60, color: "#00ffe1" },
      //   { position: 70, color: "#009dff" },
      //   { position: 80, color: "#004cff" },
      //   { position: 90, color: "#002aff" },
      //   { position: 100, color: "#0011ff" },
      //   { position: 110, color: "#5900ff" },
      //   { position: 120, color: "#9900ff" },
      //   { position: 130, color: "#c800ff" },
      //   { position: 140, color: "#d400ff" },
      //   { position: 150, color: "#ff00d0" },
      //   { position: 160, color: "#ff0066" },
      //   { position: 170, color: "#ff0000" },
      // ],
      //Preset police
      // colorMarkers: [
      //   { position: 45, color: "#ff0000" },
      //   { position: 135, color: "#0000ff" },
      // ],
    };
  },
  created(){
    this.busCol.$on('playTrack', () =>{
      this.bouleDisco();
    });
    this.busCol.$on('soundListeningEnded', () => {
      this.stopBouleDisco();
    });
    this.unwatch = this.$store.watch(
      (getters) => getters.maxTrackWidth,
      (newVal) => {
        const width = parseFloat(window.getComputedStyle(this.$refs.lightsTrack).getPropertyValue('width').split('px')[0]);
        if(Math.round(width) < newVal){
          this.width = `${newVal}px`;
        }
      }
    );
  },
  sockets: {},
  methods: {
    sendColor() {
      this.$socket.emit("changeLights", {color: this.color, id: this.lightId});
    },
    clickEvent(event) {
      const rect = event.target.getBoundingClientRect();
      const x = event.clientX - rect.left; //x position within the element.
      if (
        !(
          this.colorMarkers.find(
            (c) =>
              c.position - 3 === x || c.position === x || c.position + 3 === x
          ) || event.target.className !== "color-track"
        )
      ) {
        this.colorMarkers.push({ position: x, color: "#ffffff" });
      }
    },
    markerColorChanged(event) {
      this.colorMarkers.find((c) => c.position === event.position).color =
        event.color;
    },
    touchStart(event){
      const rect = event.target.getBoundingClientRect();
      const x = event.changedTouches[0].clientX - rect.left; //x position within the element.
      this.touchStartX = x;
      console.log(`touchstart ${this.touchStartX}`);
    },
    touchEnd(event){
      const rect = event.target.getBoundingClientRect();
      const x = event.changedTouches[0].clientX - rect.left; //x position within the element.
      if(this.touchStartX < x){
        this.colorMarkers
          .filter((e)=>e.position > this.touchStartX && e.position < x)
          .forEach((e)=>this.deleteMarker(e))
      }
    },
    deleteMarker(marker){
      const indexOfMarker = this.colorMarkers.map((e)=>e.position).indexOf(marker.position);
      this.colorMarkers.splice(indexOfMarker, 1);
    },
    bouleDisco() {
      const test = [...this.colorMarkers].sort(
        (a, b) => a.position - b.position
      );
      test.forEach((c) => {
        this.colorsTimeouts.push(
          setTimeout(() => {
            this.$socket.emit("changeLights",  {color: c.color, id: this.lightId});
          }, this.pxToSecond(c.position)*1000)
        );
      });
    },
    stopBouleDisco(){
      this.colorsTimeouts.forEach((timeoutId) => clearTimeout(timeoutId));
      this.colorsTimeouts = [];
      this.$socket.emit("changeLights",  {color: '#999', id: this.lightId})
    },
    loop(){
      this.colorsTimeouts = [];
      this.bouleDisco();
      setTimeout(() => {
        this.loop();
      }, this.pxToSecond([...this.colorMarkers].pop().position)*1000);
    },
    pxToSecond(px){
      return px / 20;
    }
  }
};
</script>
<style lang="css" scoped>
  .playButton{
    flex: 0 1 auto;
    display: flex;
    flex-direction: column;
    justify-content: center;
    height: 100%;
  }

  .playButton img{
    cursor: pointer;
    height: 50px;
    margin: 1em;
  }

  .lights-track {
    position: relative;
    display: flex;
    height: 70px;
    padding-left: 1em;
    margin-bottom: 2px;
    background-color: gray;
  }

  .color-track{
    position: relative;
    display: grid;
    height: 100%;
    width: 100%;
  }

  ColorMarker, .color-track img{
    grid-area: 1 / 1;
    pointer-events: none;
    user-select: none;
  }

  .color-track img{
    pointer-events: none;
    width: 60px;
    height: 60px;
    margin: auto;
    object-fit: cover;
    opacity: 0.1;
  }
</style>
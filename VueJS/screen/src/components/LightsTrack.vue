<template>
  <div class="color-track" id="color-track" @click="clickEvent">
    <input type="color" name="color" id="color" v-model="color" />
    {{ color }}
    <button @click="sendColor()">Send new color</button>
    <button @click="bouleDisco()">Start color track</button>
    <button @click="loop()">Start a loop with the color track</button>
    <ColorMarker
      v-for="colorMarker in colorMarkers"
      :key="colorMarker.position"
      :marker="colorMarker"
      v-on:changedcolor="markerColorChanged"
    ></ColorMarker>
  </div>
</template>
<script>
import ColorMarker from "./ColorMarker.vue";
export default {
  components: {
    ColorMarker,
  },
  data() {
    return {
      color: "#000000",
      colorMarkers: [],
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
  sockets: {},
  methods: {
    sendColor() {
      this.$socket.emit("changeLights", this.color);
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
        console.log(x);
      }
    },
    markerColorChanged(event) {
      this.colorMarkers.find((c) => c.position === event.position).color =
        event.color;
    },
    bouleDisco() {
      const test = [...this.colorMarkers].sort(
        (a, b) => a.position - b.position
      );
      test.forEach((c) => {
        setTimeout(() => {
          this.$socket.emit("changeLights", c.color);
        }, this.pxToSecond(c.position)*1000);
      });
    },
    loop(){
      this.bouleDisco();
      setTimeout(() => {
        this.loop();
      }, this.pxToSecond([...this.colorMarkers].pop().position)*1000);
    },
    pxToSecond(px){
      return px / 20;
    }
  },
};
</script>
<style lang="css" scoped>
.color-track {
  position: relative;
  width: 100%;
  height: 50px;
  background-color: gray;
}
</style>
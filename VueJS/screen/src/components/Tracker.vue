<template>
  <div class="tracker" ref="tracker"></div>
</template>
<script>
import Vue from "vue";

export default {
  name: "Tracker",
  data() {
    return {
        animationIntervalId: null,
    };
  },
  props: {
    busCol: Vue,
  },
  created() {
    this.busCol.$on("playTrack", () => {
        clearInterval(this.animationIntervalId);
        this.animationIntervalId = setInterval(this.animate,60);
    });
    this.busCol.$on("soundListeningEnded", () => {
        this.$refs.tracker.style.left = 'calc(50px + 3em - 3px)';
        clearInterval(this.animationIntervalId);
    });
  },
  computed:{
    widthFromLeft(){
      return (this.$store.getters.maxTrackWidth - (50 + 3*16 -3));
    }
  },
  methods: {
      animate(){
          if(parseFloat(window.getComputedStyle(this.$refs.tracker).getPropertyValue('left').split('px')[0],10) >= this.widthFromLeft){
              clearInterval(this.animationIntervalId);
              this.$refs.tracker.style.left = 'calc(50px + 3em - 3px)';
          }else{
            const left = parseFloat(window.getComputedStyle(this.$refs.tracker).getPropertyValue('left').split('px')[0]);
            this.$refs.tracker.style.left = `${left + 1.2}px`;
          }
      }
  },
};
</script>
<style lang="css" scoped>
    .tracker {
        pointer-events: none;
        width: 1px;
        height: 100%;
        position: absolute;
        z-index: 99;
        top: 0;
        left: calc(50px + 3em - 3px);
        background-color: yellow;
    }
</style>
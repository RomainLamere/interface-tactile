<template>
  <div class="workspace-container">
    <div class="studyo">
      <div class="playButton">
        <img v-if="!play" src="@/assets/icons/bouton-jouer.png" @click="checkIfCanPlayTrack()" alt=""/>
        <img v-else src="@/assets/icons/stop-button.png" @click="checkIfCanPlayTrack()" alt=""/>
      </div>
      <div class="compose">
        <Tracker :busCol="busCol"></Tracker>
        <LightsTrack :busCol="busCol" :lightId="1"></LightsTrack>
        <LightsTrack :busCol="busCol" :lightId="2"></LightsTrack>
        <LightsTrack :busCol="busCol" :lightId="3"></LightsTrack>
        <div class="tracks">
          <div class="tracksList">
            <TrackLine
              v-for="(n, index) in trackArray"
              :key="index" v-bind:index="index"
              v-on:addLine="addEmptyLine()"
              v-on:canPlay="playTrack($event)"
              v-on:lineIsStop="checkPlay($event)"
              v-on:lineIsStart="startButton($event)"
              :busCol="busCol"
              ></TrackLine>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import LightsTrack from './LightsTrack.vue';
import Vue from "vue";
import TrackLine from "./TrackLine";
import Tracker from "./Tracker.vue"

export default {
  name: "Workspace",
  components:{
    TrackLine,
    LightsTrack,
    Tracker
  },
  data(){
    return {
      busCol: new Vue(),
      bpm: 0,
      trackArray: [0],
      nbEventReceived : 0,
      arrayTrackEventFinishReceived : [],
      canPlay : true,
      play : false,
    }
  },
  methods:{
    addEmptyLine(){
      this.trackArray.push(this.trackArray[this.trackArray.length-1]+1);
      this.arrayTrackEventFinishReceived.push(false);
      console.log('Src added in track col');
    },
    checkIfCanPlayTrack(){
      this.busCol.$emit('checkIfCanPlayTrack', {
      });
    },
    playTrack($event){
      this.nbEventReceived ++;
      if($event === false){ // si seulement une track ne peut pas etre jouer alors on ne pourra pas lancer toutes les tracks ensembles
        this.canPlay = false;
      }
      if(this.nbEventReceived === this.trackArray.length){
        if(this.canPlay === true){ // on peut jouer parceque tout les tracks sont arretées
          this.busCol.$emit('playTrack', {
          });
          this.play = true;
        }else{
          // on arrete tous le monde
          this.busCol.$emit('stopTrack');
          this.busCol.$emit('soundListeningEnded');
          this.play = false;
        }
        this.nbEventReceived=0;
        this.canPlay = true;
      }
    },
    checkPlay($event){
      this.arrayTrackEventFinishReceived[$event] = true;
      let pause = true;
      for(let i = 0 ; i < this.arrayTrackEventFinishReceived.length-1 ; i++){
        if(this.arrayTrackEventFinishReceived[i] === false){
          pause = false;
        }
      }
      if(pause) {
        this.play = false;
      }
    },
    startButton($event){
      this.arrayTrackEventFinishReceived[$event] = false;
      let pause = true;
      for(let i = 0 ; i < this.arrayTrackEventFinishReceived.length-1 ; i++){
        if(this.arrayTrackEventFinishReceived[i] === false){
          pause = false;
        }
      }
      if(pause) {
        this.play = false;
      }
    }
  },

};
</script>

<style lang="css" scoped>
  .workspace-container{
    width: 100%;
    overflow: hidden;
  }

  .studyo{
    height: 100%;
    width: 100%;
    display: flex;
  }

  .studyo ::-webkit-scrollbar{
    height: 10px;
    width: 10px;
    background-color: #535353;
  }

  .studyo ::-webkit-scrollbar-thumb{
    background-color: #ffffff1f;
    border-radius: 10px;
    height: 8px;
  }

  .playButton{
    flex: 0 1 auto;
    display: flex;
    flex-direction: column;
    justify-content: center;
    height: 100%;
  }
  
  .playButton img{
    cursor: pointer;
    height: 75px;
    margin: 1em;
  }

  .compose{
    position: relative;
    flex: 1 1 100%;
    display: flex;
    flex-direction: column;
    overflow-x: scroll;
    background-color: #535353;
  }

  .tracks{
    height: 100%;
    width: max-content;
    padding: 1em;
    overflow: visible;
    overflow-y: scroll;
  }

  .tracksList{
    flex: 1 1 auto;
  }
</style>

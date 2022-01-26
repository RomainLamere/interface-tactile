<template>
  <drop class="drop-zone" @drop="trackDropped">
    <div :class="`instru ${trackZone}`">
      <img
        v-if="instrument !== ''"
        :src="require(`@/assets/icons/${instrument}.png`)"
        alt=""
      />
      <template v-else>
        <div>select track<br>from table</div>
      </template>
    </div>
    <div class="track"></div>
  </drop>
</template>
<script>
import { Drop } from "vue-easy-dnd";

export default {
  components: {
    Drop,
  },
  data() {
    return {
      trackZone: "",
      instrument: "",
    };
  },
  methods: {
    trackDropped(e) {
      this.trackZone = e.data.zone;
      this.instrument = e.data.instru.instru;
    },
  }
};
</script>
<style lang="css" scoped>
.drop-zone {
  display: inline-flex;
  min-width: 100%;
}

.drop-in{
    box-shadow: 2px 1px 15px #0095ff;
    box-shadow: 2px 1px 15px #0095ff;
}

.instru {
  display: flex;
  justify-content: center;
  align-items: center;
  width: 100px;
  height: 70px;
  margin-right: 10px;
  background-color: #444444;
  border-radius: 4px;
  box-shadow: 2px 2px 3px #222222;
}

.A{
    border: 2px solid #d66f6f;
}

.B{
    border: 2px solid #6fd674;
}

.C{
    border: 2px solid #6f87d6;
}

.D{
    border: 2px solid #d4d66f;
}

.instru img {
  width: 60px;
  height: 60px;
}

.track {
  min-width: 90%;
  height: 70px;
  background-color: #444444;
  border-radius: 4px;
  box-shadow: 2px 2px 3px #222222;
}

.track audio{
    display: none;
}
</style>
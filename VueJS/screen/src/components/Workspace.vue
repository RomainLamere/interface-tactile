<template>
  <div>Message: {{ msg }}</div>
</template>

<script>
export default {
  name: "Workspace",
  data(){
      return {
          msg: ''
      }
  },
  sockets:{},
  created() {
    this.$options.sockets.eventToScreen = (data) => {
      console.log(`Test subscribe: ${data}`);
      this.msg = data;
    };
  },
};
</script>

<style lang="css" scoped>
</style>
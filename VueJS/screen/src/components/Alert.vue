<template>
  <div id="alert">
    Nouveau son zone {{zone}}
  </div>
</template>
<script>
export default {
  name: "Alert",
  props:{
    zone: String
  },
}
</script>
<style lang="css" scoped>
#alert{
  background-color: #6fd674;
}
</style>

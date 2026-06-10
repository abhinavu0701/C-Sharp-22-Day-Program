<script setup>
import { ref, onMounted } from 'vue'

// Reactive array that will hold patients.
const patients = ref([])

onMounted(async () => {
  const response =
    await fetch('https://localhost:7101/api/patients')

  patients.value =
    await response.json()
})
</script>

<template>

  <h1>CareBridge Patients</h1>

  <table border="1">

    <tr>
      <!-- ✅ CHANGED HEADERS ONLY -->
      <th>Department</th>
      <th>InPatient Count</th>
      <th>OutPatient Count</th>
      <th>ED Count</th>
      <th>Total Count</th>
    </tr>

    <!-- Loop through all patients -->

    <tr
  v-for="(p, index) in patients"
  :key="p.departmentName"
  :style="{ color: index === 0 ? 'red' : 'black' }">

  <td>{{ p.departmentName }}</td>
  <td>{{ p.inPatientCount }}</td>
  <td>{{ p.outPatientCount }}</td>
  <td>{{ p.edCount }}</td>
  <td>{{ p.totalCount }}</td>

</tr>

  </table>

</template>

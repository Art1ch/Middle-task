import { Component, Input, OnChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BaseChartDirective } from 'ng2-charts';
import {
  Chart,
  LineController,
  LineElement,
  PointElement,
  LinearScale,
  TimeScale,
  CategoryScale,
  Legend,
  Tooltip,
  ChartConfiguration
} from 'chart.js';
import 'chartjs-adapter-date-fns';
import { SensorDataItem } from '../../models/sensor-data.model';

Chart.register(
  LineController,
  LineElement,
  PointElement,
  LinearScale,
  TimeScale,
  CategoryScale,
  Legend,
  Tooltip
);

@Component({
  selector: 'app-room-chart',
  standalone: true,
  imports: [CommonModule, BaseChartDirective],
  templateUrl: './room-chart.html'
})
export class RoomChartComponent implements OnChanges {

  @Input() room!: string;
  @Input() data: any;

  chartData: ChartConfiguration<'line'>['data'] = {
    datasets: []
  };

  chartOptions: ChartConfiguration<'line'>['options'] = {
    responsive: true,
    maintainAspectRatio: false,

    layout: {
      padding: {
        left: 5,
        right: 5
      }
    },

    scales: {
      x: {
        type: 'time',
        time: {
          unit: 'minute'
        }
      },

      y: {
        beginAtZero: false
      }
    },

    plugins: {
      legend: {
        position: 'bottom'
      }
    }
  };


  ngOnChanges() {
    if (!this.data) {
      return;
    }
    console.log('ROOM CHART DATA', this.data);

    const energy =
      this.data.energy?.sensorsData ?? [];
    const air =
      this.data.air?.sensorsData ?? [];
      
    console.log('ENERGY', energy);
    console.log('AIR', air);

    const datasets = [];

    if (energy.length) {
      datasets.push({
        label: 'Energy',
        data: energy.map((x: SensorDataItem) => ({
          x: x.timestamp,
          y: x.payload.energy
        }))
      });
    }

    if (air.length) {
      datasets.push(
        {
          label: 'CO2',
          data: air.map((x: SensorDataItem) => ({
            x: x.timestamp,
            y: x.payload.co2
          }))
        },
        {
          label: 'PM2.5',
          data: air.map((x: SensorDataItem) => ({
            x: x.timestamp,
            y: x.payload.pm25
          }))
        },
        {
          label: 'Humidity',
          data: air.map((x: SensorDataItem) => ({
            x: x.timestamp,
            y: x.payload.humidity
          }))
        }
      );
    }
    this.chartData = {
      datasets
    }

    console.log('DATASETS', this.chartData);
  }
}
export interface SensorDataResponse {
  sensorsData: SensorDataItem[];
}

export interface SensorDataItem {
  id: string;
  dataType: SensorTypeEnum;
  placementName: string;
  timestamp: string;
  payload: any;
}

export interface GetSensorsDataRequest {
  type?: string;
  placementName?: string;
  from?: string;
  to?: string;
  page: number;
  pageSize: number;
}

export enum SensorTypeEnum{
  energy = 'energy',
  airQuality = 'air_quality'
}

export interface SensorFilter {
  type?: string;
  placementName?: string;
  from?: Date | null;
  to?: Date | null;
  page: number;
  pageSize: number;
}
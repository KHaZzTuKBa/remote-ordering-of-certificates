import { RequestStatus } from './request-status.enum';

export interface RequestInformation {
  id: string;
  name: string;
  fullRequestStatus: RequestStatus;
  date: string;
  filePath: string;
}


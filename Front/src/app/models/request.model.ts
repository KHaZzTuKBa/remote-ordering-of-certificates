import { RequestStatus } from './request-status.enum';

export interface Request {
  id: string;
  requestStatus: RequestStatus;
  date: string;
}


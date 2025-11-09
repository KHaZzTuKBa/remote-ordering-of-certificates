import { RequestStatus } from './request-status.enum';
import { ReceivingFormat } from './receiving-format.enum';

export interface Request {
  id: string;
  requestStatus: RequestStatus;
  date: string;
  receivingFormat: ReceivingFormat;
}


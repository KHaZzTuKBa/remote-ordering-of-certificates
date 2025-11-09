import { RequestStatus } from './request-status.enum';
import { ReceivingFormat } from './receiving-format.enum';

export interface RequestInformation {
  id: string;
  name: string;
  fullRequestStatus: RequestStatus;
  date: string;
  filePath: string;
  receivingFormat: ReceivingFormat;
}


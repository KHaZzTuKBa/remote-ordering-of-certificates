import { ReceivingFormat } from './receiving-format.enum';

export interface CreateRequestDTO {
  studentId: number;
  fullName: string;
  course: number;
  receivingFormat: ReceivingFormat;
}


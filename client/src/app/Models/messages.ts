export interface Message {
    id: number;
    senderId: number;
    senderName: string;
    senderPhotoUrl: string;
    reciverId: number;
    reciverName: string;
    reciverIdPhotoUrl: string;
    content: string;
    dateRead: Date;
    messageSent: Date;
  }

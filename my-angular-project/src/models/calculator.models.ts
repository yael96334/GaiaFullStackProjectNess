export interface Operation {
  code: string;
  expression:string;
  type:string;
//   name: string;
  symbol: string; 
}

export interface CalculationRequest {
  fieldA: string;
  fieldB: string;
  operationType: string;
  symbol:string
}


export interface CalculationHistoryItem {
  fieldA: number;
  fieldB: number;
  result: number;
  symbol: string;
  createdAt: string;
}
export interface CalculationResult {
  result: number;
  lastThree: CalculationHistoryItem[];
  monthlyCount: number;
  lastId?: number; // <-- הוסיפי את השדה הזה
}
// export interface CalculationResult {
    
//   lastThree: CalculationHistoryItem[];
//   monthlyCount: number;
// }
export interface CalculationResponse {
    
  result: {
    result:number,
     lastId: number;
  };
 
}
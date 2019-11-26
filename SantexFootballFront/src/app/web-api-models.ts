export class AmmountAnswer {
    total: string;
}
export class ImportAnswer {
    message: string;
}
export class CompetitionListDTO {
    Competitions: CompetitionList;
    Headers: Headers;
    Success: boolean;
    Message: string;
}
export class CompetitionList {
    count: number;
    competitions: Competition[];
}
export class Headers {
    RequestsAvailable: string;
    ApiVersion: string;
    UserName: string;
}
export class Competition {
    id: number;
    name: string;
    code: string;
    areaName: string;
}

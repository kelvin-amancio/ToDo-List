import { ITaskItem } from "./ITaskItem";

export interface ITaskItemPaged {
        tasks: ITaskItem[];
        totalItems: number;
        totalPages: number;
        currentPage: number;
        pageSize: number;
}
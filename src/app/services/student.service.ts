import { Injectable } from '@angular/core';
import { StudentModel } from '../models/student.model';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class StudentService {
  
  // students: StudentModel[] = [
  //   { id: 1, name: "Abdallah Wahbah", age: 23, address: "Cairo" },
  //   { id: 2, name: "Ahmed Ali", age: 22, address: "Giza" },
  //   { id: 3, name: "Omar Hassan", age: 24, address: "Alexandria" },
  //   { id: 4, name: "Mahmoud Tarek", age: 21, address: "Riyadh" },
  //   { id: 5, name: "Youssef Ibrahim", age: 25, address: "Dubai" },
  //   { id: 6, name: "Mostafa Adel", age: 23, address: "Sharjah" },
  //   { id: 7, name: "Ali Samir", age: 22, address: "Doha" },
  //   { id: 8, name: "Karim Nader", age: 24, address: "Abu Dhabi" },
  //   { id: 9, name: "Hassan Mohamed", age: 26, address: "Cairo" },
  //   { id: 10, name: "Eslam Fathy", age: 23, address: "Alexandria" },
  //   { id: 11, name: "Ayman Saad", age: 22, address: "Riyadh" },
  //   { id: 12, name: "Nader Hossam", age: 24, address: "Giza" },
  //   { id: 13, name: "Tamer Gamal", age: 25, address: "Dubai" },
  //   { id: 14, name: "Hossam Fathy", age: 23, address: "Cairo" },
  //   { id: 15, name: "Ibrahim Khaled", age: 22, address: "Jeddah" },
  //   { id: 16, name: "Khaled Mostafa", age: 24, address: "Abu Dhabi" },
  // ];
  students: StudentModel[] = [];
  baseURL: string = 'https://localhost:7224/api/Students/';
  token = localStorage.getItem("token");

  constructor(private http: HttpClient){
    
  }

  
  getAll(){
    return this.http.get<StudentModel[]>(this.baseURL, {
      headers:{
        Authorization: `Bearer ${this.token}`
      }
    })
  }
  getById(id: number){
    return this.http.get<StudentModel>(this.baseURL + id, {
      headers:{
        Authorization: `Bearer ${this.token}`
      }
    });
  }
  addStudent(std: StudentModel){
    return this.http.post<StudentModel>(this.baseURL, std, {
      headers:{
        Authorization: `Bearer ${this.token}`
      }
    })
  }
  updateStudent(selectedStudent: StudentModel){
    return this.http.put(this.baseURL+selectedStudent.id, selectedStudent, {
      headers:{
        Authorization: `Bearer ${this.token}`
      }
    });
  }
  deleteStudent(id: number){
    return this.http.delete(this.baseURL + id, {
      headers:{
        Authorization: `Bearer ${this.token}`
      }
    });
  }
}

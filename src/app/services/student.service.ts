import { Injectable } from '@angular/core';
import { StudentModel } from '../models/student.model';

@Injectable({
  providedIn: 'root',
})
export class StudentService {
  students: StudentModel[] = [
    { id: 1, name: "Abdallah Wahbah", age: 23, address: "Cairo" },
    { id: 2, name: "Ahmed Ali", age: 22, address: "Giza" },
    { id: 3, name: "Omar Hassan", age: 24, address: "Alexandria" },
    { id: 4, name: "Mahmoud Tarek", age: 21, address: "Riyadh" },
    { id: 5, name: "Youssef Ibrahim", age: 25, address: "Dubai" },
    { id: 6, name: "Mostafa Adel", age: 23, address: "Sharjah" },
    { id: 7, name: "Ali Samir", age: 22, address: "Doha" },
    { id: 8, name: "Karim Nader", age: 24, address: "Abu Dhabi" },
    { id: 9, name: "Hassan Mohamed", age: 26, address: "Cairo" },
    { id: 10, name: "Eslam Fathy", age: 23, address: "Alexandria" },
    { id: 11, name: "Ayman Saad", age: 22, address: "Riyadh" },
    { id: 12, name: "Nader Hossam", age: 24, address: "Giza" },
    { id: 13, name: "Tamer Gamal", age: 25, address: "Dubai" },
    { id: 14, name: "Hossam Fathy", age: 23, address: "Cairo" },
    { id: 15, name: "Ibrahim Khaled", age: 22, address: "Jeddah" },
    { id: 16, name: "Khaled Mostafa", age: 24, address: "Abu Dhabi" },
  ];
  getAll(){
    return this.students;
  }
  getById(id: number){
    return this.students.find(std => std.id === id);
  }
  addStudent(std: StudentModel){
    this.students.unshift(std);
  }
  updateStudent(selectedStudent: StudentModel){
    let std =  this.students.find(x => x.id === selectedStudent.id);
    if(std){
      std.name = selectedStudent.name
      std.age = +selectedStudent.age
      std.address = selectedStudent.address;
    }
  }
  deleteStudent(id: number){
    this.students = this.students.filter(std => std.id !== id);
  }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace UniversityRegistrar
{
  public class Student
  {
    private int _id;
    private string _name;
    private DateTime _enrolldate;

    public Student(string Name, DateTime EnrollDate, int Id = 0)
    {
      _id = Id;
      _name = Name;
      _enrolldate = EnrollDate;
    }

    public override bool Equals(System.Object otherStudent)
    {
      if (!(otherStudent is Student))
      {
        return false;
      }
      else
      {
        Student newStudent = (Student) otherStudent;
        bool idEquality = (_id == newStudent.GetId());
        bool nameEquality = (_name == newStudent.GetName());
        bool enrollDateEquality = (_enrolldate = newStudent.GetEnrollDate());
        return (idEquality && nameEquality && enrollDateEquality);
      }
    }

    public int GetId()
    {
      return _id;
    }

    public string GetName()
    {
      return _name;
    }

    public DateTime GetEnrollDate()
    {
      return _enrolldate;
    }

    public void SetName(string newName)
    {
      _name = newName;
    }

    public void SetEnrollDate(DateTime newEnrollDate)
    {
      _enrolldate = newEnrollDate;
    }
  }
}

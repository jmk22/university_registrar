using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace UniversityRegistrar
{
  public class Course
  {
    private int _id;
    private string _courseName;
    private string _courseNumber;

    public Course(string CourseName, string CourseNumber, int Id = 0)
    {
      _id = Id;
      _courseName = CourseName;
      _courseNumber = CourseNumber;
    }

    public override bool Equals(System.Object otherCourse)
    {
      if (!(otherCourse is Course))
      {
        return false;
      }
      else
      {
        Course newCourse = (Course) otherCourse;
        bool idEquality = (_id == newCourse.GetId());
        bool nameEquality = (_courseName == newCourse.GetCourseName());
        bool numberEquality = (_courseNumber = newCourse.GetCourseNumber());
        return (idEquality && nameEquality && numberEquality);
      }
    }

    public int GetId()
    {
      return _id;
    }

    public string GetCourseName()
    {
      return _courseName;
    }

    public string GetCourseNumber()
    {
      return _courseNumber;
    }

    public void SetCourseName(string newName)
    {
      _courseName = newName;
    }

    public void SetEnrollDate(string newCourseNumber)
    {
      _courseNumber = newEnrollDate;
    }
  }
}

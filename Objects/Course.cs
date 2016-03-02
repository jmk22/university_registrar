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

    private static SqlConnection conn = DB.Connection();


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
        bool numberEquality = (_courseNumber == newCourse.GetCourseNumber());
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

    public void SetCourseNumber(string newCourseNumber)
    {
      _courseNumber = newCourseNumber;
    }
    public static List<Course> GetAll()
    {
      List<Course> allCourses = new List<Course>{};

      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM courses;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int courseId = rdr.GetInt32(0);
        string courseName = rdr.GetString(1);
        string courseNumber = rdr.GetString(2);
        Course newCourse = new Course(courseName, courseNumber, courseId);
        allCourses.Add(newCourse);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allCourses;
    }

    public void Save()
    {
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO courses (course_name, course_number) OUTPUT INSERTED.id VALUES (@CourseName, @CourseNumber)", conn);

      SqlParameter nameParam = new SqlParameter();
      nameParam.ParameterName = "@CourseName";
      nameParam.Value = this.GetCourseName();

      SqlParameter numberParam = new SqlParameter();
      numberParam.ParameterName = "@CourseNumber";
      numberParam.Value = this.GetCourseNumber();


      cmd.Parameters.Add(nameParam);
      cmd.Parameters.Add(numberParam);

      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        _id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public static void DeleteAll()
    {
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM courses;", conn);
      cmd.ExecuteNonQuery();
      if (conn != null)
      {
        conn.Close();
      }
    }

    public static Course Find(int searchId)
    {
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM courses WHERE id = @CourseId", conn);

      SqlParameter idParam = new SqlParameter();
      idParam.ParameterName = "@CourseId";
      idParam.Value = searchId.ToString();

      cmd.Parameters.Add(idParam);

      rdr = cmd.ExecuteReader();

      int foundCourseId = 0;
      string foundName = null;
      string foundNumber = null;

      while(rdr.Read())
      {
        foundCourseId = rdr.GetInt32(0);
        foundName = rdr.GetString(1);
        foundNumber = rdr.GetString(2);
      }

      Course foundCourse = new Course(foundName, foundNumber, foundCourseId);

      if (rdr != null)
      {
        rdr.Close();
      }

      if (conn != null)
      {
        conn.Close();
      }

      return foundCourse;
    }

  }
}

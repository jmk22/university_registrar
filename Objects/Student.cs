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

    private static SqlConnection conn = DB.Connection();

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
        bool enrollDateEquality = (_enrolldate == newStudent.GetEnrollDate());
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

    public static List<Student> GetAll()
    {
      List<Student> allStudents = new List<Student>{};

      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM students;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int studentId = rdr.GetInt32(0);
        string studentName = rdr.GetString(1);
        DateTime studentEnrollDate = rdr.GetDateTime(2);
        Student newStudent = new Student(studentName, studentEnrollDate, studentId);
        allStudents.Add(newStudent);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allStudents;
    }

    public void Save()
    {
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO students (name, enroll_date) OUTPUT INSERTED.id VALUES (@StudentName, @StudentEnrollDate)", conn);

      SqlParameter nameParam = new SqlParameter();
      nameParam.ParameterName = "@StudentName";
      nameParam.Value = this.GetName();

      SqlParameter enrollDateParam = new SqlParameter();
      enrollDateParam.ParameterName = "@StudentEnrollDate";
      enrollDateParam.Value = this.GetEnrollDate();

      cmd.Parameters.Add(nameParam);
      cmd.Parameters.Add(enrollDateParam);

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
      SqlCommand cmd = new SqlCommand("DELETE FROM students;", conn);
      cmd.ExecuteNonQuery();
      if (conn != null)
      {
        conn.Close();
      }
    }

    public static Student Find(int searchId)
    {
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM students WHERE id = @StudentId", conn);

      SqlParameter idParam = new SqlParameter();
      idParam.ParameterName = "@StudentId";
      idParam.Value = searchId.ToString();

      cmd.Parameters.Add(idParam);

      rdr = cmd.ExecuteReader();

      int foundStudentId = 0;
      string foundName = null;
      DateTime foundEnrollDate = DateTime.Now;

      while(rdr.Read())
      {
        foundStudentId = rdr.GetInt32(0);
        foundName = rdr.GetString(1);
        foundEnrollDate = rdr.GetDateTime(2);
      }

      Student foundStudent = new Student(foundName, foundEnrollDate, foundStudentId);

      if (rdr != null)
      {
        rdr.Close();
      }

      if (conn != null)
      {
        conn.Close();
      }

      return foundStudent;
    }

    public void AddCourse(Course newCourse)
    {
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO enrollments (course_id, student_id) VALUES (@CourseId, @StudentId)", conn);

      SqlParameter CourseIdParam = new SqlParameter();
      CourseIdParam.ParameterName = "@CourseId";
      CourseIdParam.Value = newCourse.GetId().ToString();

      SqlParameter StudentIdParam = new SqlParameter();
      StudentIdParam.ParameterName = "@StudentId";
      StudentIdParam.Value = this.GetId().ToString();

      cmd.Parameters.Add(CourseIdParam);
      cmd.Parameters.Add(StudentIdParam);

      rdr = cmd.ExecuteReader();

      if (rdr != null)
      {
        rdr.Close();
      }

      if (conn != null)
      {
        conn.Close();
      }
    }

    public List<Course> GetCourses()
    {
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT courses.* FROM students  JOIN enrollments ON (students.id = enrollments.student_id) JOIN courses ON (enrollments.course_id = courses.id) WHERE students.id = @StudentId");
      SqlParameter StudentIdParam = new SqlParameter();
      StudentIdParam.ParameterName = "@StudentId";
      StudentIdParam.Value = this.GetId().ToString();

      cmd.Parameters.Add(StudentIdParam);

      rdr = cmd.ExecuteReader();

      List<Course> courses = new List<Course>{};

      while(rdr.Read())
      {
        int courseId = rdr.GetInt32(0);
        string courseName = rdr.GetString(1);
        string courseNumber = rdr.GetString(2);
        Course newCourse = new Course(courseName, courseNumber, courseId);
        courses.Add(newCourse);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return courses;
    }

  }
}

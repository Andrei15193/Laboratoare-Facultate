package aop2.domain;

import java.util.Observable;

import aop2.persistence.AllCourses;
import aop2.persistence.AllMarks;
import aop2.persistence.AllStudents;
import aop2.persistence.RepositoryException;
import aop2.validator.CourseValidator;
import aop2.validator.MarkValidator;
import aop2.validator.StudentValidator;
import aop2.validator.ValidatorException;


public class MarksApplication extends Observable
{
    public MarksApplication(final AllStudents allStudents,
                    final AllCourses allCourses, final AllMarks allMarks)
    {
        this.allStudents = allStudents;
        this.allCourses = allCourses;
        this.allMarks = allMarks;
    }

    public void addStudent(final String name, final String password)
                    throws RepositoryException, ValidatorException
    {
        final String[] errors;
        final Student newStudent = new Student(name, password);
        errors = this.studentValidator.validate(newStudent);
        if (errors.length > 0)
            throw new ValidatorException(errors);
        this.allStudents.nowInclude(newStudent);
        this.setChanged();
        this.notifyObservers(newStudent);
    }

    public void addCourse(final String name) throws RepositoryException,
                    ValidatorException
    {
        final String[] errors;
        final Course newCourse = new Course(name);
        errors = this.courseValidator.validate(newCourse);
        if (errors.length > 0)
            throw new ValidatorException(errors);
        this.allCourses.nowInclude(newCourse);
        this.setChanged();
        this.notifyObservers(newCourse);
    }

    public void addMark(final String studentName, final String courseName,
                    final int mark) throws RepositoryException,
                    ValidatorException
    {
        final String[] errors;
        final Mark newMark;
        final Student student = this.getStudentFromRepository(studentName);
        final Course course = this.getCourseFromRepository(courseName);
        newMark = new Mark(student, course, mark);
        errors = this.markValidator.validate(newMark);
        if (errors.length > 0)
            throw new ValidatorException(errors);
        this.allMarks.nowInclude(newMark);
        this.setChanged();
        this.notifyObservers(newMark);
    }

    public Mark[] getMarksForStudent(final String name)
                    throws RepositoryException
    {
        return this.allMarks.from(this.getStudentFromRepository(name));
    }

    public Course[] getAllCourses() throws RepositoryException
    {
        return this.allCourses.get();
    }

    public Student getStudent(final String name, final String password)
                    throws RepositoryException
    {
        return this.allStudents.with(name, password);
    }

    public void setCourseValidator(CourseValidator validator)
    {
        this.courseValidator = validator;
    }

    public void setMarkValidator(MarkValidator validator)
    {
        this.markValidator = validator;
    }

    public void setStudentValidator(StudentValidator validator)
    {
        this.studentValidator = validator;
    }

    private Course getCourseFromRepository(final String courseName)
                    throws RepositoryException
    {
        final Course course = this.allCourses.with(courseName);
        if (course == null)
            throw new RepositoryException(
                            "The course does not exist in the repository!");
        else
            return course;
    }

    private Student getStudentFromRepository(final String studentName)
                    throws RepositoryException
    {
        final Student student = this.allStudents.with(studentName);
        if (student == null)
            throw new RepositoryException(
                            "The student does not exist in the repository!");
        else
            return student;
    }

    private CourseValidator courseValidator;
    private MarkValidator markValidator;
    private StudentValidator studentValidator;
    private final AllStudents allStudents;
    private final AllCourses allCourses;
    private final AllMarks allMarks;
}

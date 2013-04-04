package aop2.domain;

import java.io.Serializable;

public class Mark implements Serializable
{
    public Mark(final Student student, final Course course, final int mark)
    {
        this.student = student;
        this.course = course;
        this.mark = mark;
    }

    public Student getStudent()
    {
        return this.student;
    }

    public Course getCourse()
    {
        return this.course;
    }

    public Integer getMark()
    {
        return this.mark;
    }

    @Override
    public String toString()
    {
        return this.student.getName() + " " + this.course.getName() + " "
                        + this.mark;
    }

    private final Student student;
    private final Course course;
    private final Integer mark;
    private static final long serialVersionUID = 1L;
}

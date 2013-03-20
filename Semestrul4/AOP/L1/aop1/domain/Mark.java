package aop1.domain;

import java.io.Serializable;

import org.apache.log4j.Level;

import aop1.Application;

public class Mark implements Serializable
{
    public Mark(final Student student, final Course course, final int mark)
    {
        Application.logger.log(Level.INFO, "Mark.Mark(" + student
                        + " :Student, " + course + " :Course, " + mark
                        + " :int)");
        this.student = student;
        this.course = course;
        this.mark = mark;
    }

    public Student getStudent()
    {
        Application.logger.log(Level.INFO, "Mark.getStudent()");
        return this.student;
    }

    public Course getCourse()
    {
        Application.logger.log(Level.INFO, "Mark.getCourse()");
        return this.course;
    }

    public Integer getMark()
    {
        Application.logger.log(Level.INFO, "Mark.getMark()");
        return this.mark;
    }

    @Override
    public String toString()
    {
        Application.logger.log(Level.INFO, "Mark.toString()");
        return this.student.getName() + " " + this.course.getName() + " "
                        + this.mark;
    }

    private final Student student;
    private final Course course;
    private final Integer mark;
    private static final long serialVersionUID = 1L;
}

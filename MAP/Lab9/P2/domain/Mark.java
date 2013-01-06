package domain;

import java.io.Serializable;

public class Mark implements Serializable
{
    public Mark(final String courseName, final String studentName,
                    final int mark)
    {
        this.courseName = courseName;
        this.studentName = studentName;
        this.mark = mark;
    }

    public final String getCourseName()
    {
        return this.courseName;
    }

    public final String getStudentName()
    {
        return this.studentName;
    }

    public final int getMark()
    {
        return this.mark;
    }

    @Override
    public boolean equals(Object object)
    {
        Mark mark;
        boolean result = false;
        if (object instanceof Mark)
        {
            mark = (Mark)object;
            result = this.courseName.equals(mark.getCourseName())
                            && this.studentName.equals(mark.getStudentName())
                            && this.mark == mark.mark;
        }
        return result;
    }

    @Override
    public String toString()
    {
        return this.courseName + " " + this.studentName + " " + this.mark;
    }

    private final String courseName;
    private final String studentName;
    private final int mark;
    private static final long serialVersionUID = 1L;
}

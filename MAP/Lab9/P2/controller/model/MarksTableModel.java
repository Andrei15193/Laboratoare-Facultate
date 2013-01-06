package controller.model;

import java.util.ArrayList;

import javax.swing.table.AbstractTableModel;

import data.iterators.StreamIterator;
import domain.Mark;

public class MarksTableModel extends AbstractTableModel
{
    public MarksTableModel()
    {
        this.rows = new ArrayList<String[]>();
    }

    public MarksTableModel(StreamIterator<Mark> iterator)
    {
        Mark mark;
        this.rows = new ArrayList<String[]>();
        while (iterator.hasNext())
        {
            mark = iterator.next();
            this.rows.add(new String[] {mark.getCourseName(),
                            mark.getMark() + ""});
        }
    }

    public void addMarks(StreamIterator<Mark> iterator)
    {
        final int insertedRowsIndexStart = this.rows.size() + 1;
        Mark mark;
        while (iterator.hasNext())
        {
            mark = iterator.next();
            this.rows.add(new String[] {mark.getCourseName(),
                            mark.getMark() + ""});
        }
        this.fireTableRowsInserted(insertedRowsIndexStart, this.rows.size());
    }

    public void addMark(Mark mark)
    {
        final int insertedRowIndex = this.rows.size() + 1;
        this.rows.add(new String[] {mark.getCourseName(), mark.getMark() + ""});
        this.fireTableRowsInserted(insertedRowIndex, insertedRowIndex);
    }

    @Override
    public String getColumnName(int column)
    {
        if (column == 0)
            return "Course";
        else
            return "Mark";
    };

    @Override
    public int getColumnCount()
    {
        return 2;
    }

    @Override
    public int getRowCount()
    {
        return this.rows.size();
    }

    @Override
    public Object getValueAt(int row, int column)
    {
        return this.rows.get(row)[column];
    }

    private final ArrayList<String[]> rows;
    private static final long serialVersionUID = 1L;
}

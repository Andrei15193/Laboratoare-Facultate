package repository;

import utils.Pair;
import domain.Deposit;

public interface DepositRepository extends Iterable<Deposit>
{
    boolean add(Deposit entity);

    void clear();

    boolean contains(Deposit entity);

    Deposit find(Pair<String, String> key);

    Iterable<Deposit> getDepositsForPerson(String personId);

    boolean isEmpty();

    @Override
    data.iterators.StreamIterator<Deposit> iterator();

    Deposit remove(Pair<String, String> key);

    int size();
}

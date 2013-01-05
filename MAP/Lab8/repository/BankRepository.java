package repository;

import java.util.ArrayList;

import utils.Pair;
import domain.Bank;
import domain.Deposit;

public interface BankRepository extends Iterable<Bank>
{
    boolean add(Bank entity);

    void clear();

    boolean contains(Bank entity);

    Bank find(String key);

    ArrayList<Pair<Deposit, Bank>> getBanksForDeposits(
                    Iterable<Deposit> deposits);

    boolean isEmpty();

    @Override
    data.iterators.StreamIterator<Bank> iterator();

    Bank remove(String key);

    int size();
}

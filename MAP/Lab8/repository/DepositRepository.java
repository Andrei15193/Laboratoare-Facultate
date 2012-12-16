package repository;

public interface DepositRepository extends java.io.Serializable
{
    void add(domain.Deposit deposit);

    domain.Deposit find(String bankName, String personId);

    controller.EntityReader<domain.Deposit> getReader();

    controller.EntityReader<domain.Deposit> getReader(
                    repository.CheckMethod checkMethod);
}

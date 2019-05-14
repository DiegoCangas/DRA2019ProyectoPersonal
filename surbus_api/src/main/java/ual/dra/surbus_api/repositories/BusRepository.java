package ual.dra.surbus_api.repositories;

import java.util.Set;

import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.CrudRepository;
import org.springframework.data.rest.core.annotation.RepositoryRestResource;

import ual.dra.surbus_api.modelos.Bus;

@RepositoryRestResource()
public interface BusRepository extends CrudRepository<Bus, Long> {
    @Query(value = "Select * From bus Where nombre = ?1", nativeQuery = true)
    Set<Bus> findByNombre(String nombre);

    @Query(value = "Select * From bus Where num_linea = ?1", nativeQuery = true)
    Set<Bus> findByLinea(int numLinea);
}
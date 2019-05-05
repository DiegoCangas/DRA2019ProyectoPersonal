package ual.dra.surbus_api.repositories;

import org.springframework.data.repository.CrudRepository;
import org.springframework.data.rest.core.annotation.RepositoryRestResource;

import ual.dra.surbus_api.modelos.Bus;

@RepositoryRestResource()
public interface BusRepository extends CrudRepository<Bus, Long> {
    
}
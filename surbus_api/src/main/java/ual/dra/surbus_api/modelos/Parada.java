package ual.dra.surbus_api.modelos;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.JoinTable;
import javax.persistence.ManyToMany;
import javax.persistence.Table;
import javax.persistence.JoinColumn;

import java.util.Set;

@Entity
@Table (name = "parada")
public class Parada{
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    private int numeroParada;
    private String name;
    private String url;
    @ManyToMany
    @JoinTable(
        name = "linea_x_parada",
        joinColumns = {@JoinColumn(name = "parada_id")},
        inverseJoinColumns = {@JoinColumn(name = "bus_id")}
    )
    private Set<Bus> lineas;
    
    public Parada(){

    }

    /**
     * @return the name
     */
    public String getName() {
        return name;
    }

    /**
     * @param name the name to set
     */
    public void setName(String name) {
        this.name = name;
    }

    /**
     * @return the url
     */
    public String getUrl() {
        return url;
    }

    /**
     * @param url the url to set
     */
    public void setUrl(String url) {
        this.url = url;
    }
    /**
     * @return the numeroParada
     */
    public int getNumeroParada() {
        return numeroParada;
    }

    /**
     * @param numeroParada the numeroParada to set
     */
    public void setNumeroParada(int numeroParada) {
        this.numeroParada = numeroParada;
    }
    
}

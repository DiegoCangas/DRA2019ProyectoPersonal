package ual.dra.surbus_api.modelos;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.JoinTable;
import javax.persistence.ManyToMany;
import javax.persistence.JoinColumn;

import java.util.Set;

@Entity
public class Parada{
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    private String name;
    private String url;
    private double longitud;
    private double latitud;
    @ManyToMany
    @JoinTable(
        name = "lineas_de_paradas",
        joinColumns = {@JoinColumn(name = "parada_id")},
        inverseJoinColumns = {@JoinColumn(name = "bus_id")}
    )
    private Set<Bus> lineas;
    
    public Parada(){

    }
    public double distanciaPlana(Parada b){
        return Math.sqrt(Math.pow(longitud-b.longitud,2)+Math.pow(latitud-b.latitud,2));
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
     * @return the longitud
     */
    public double getLongitud() {
        return longitud;
    }

    /**
     * @param longitud the longitud to set
     */
    public void setLongitud(double longitud) {
        this.longitud = longitud;
    }

    /**
     * @return the latitud
     */
    public double getLatitud() {
        return latitud;
    }

    /**
     * @param latitud the latitud to set
     */
    public void setLatitud(double latitud) {
        this.latitud = latitud;
    }
    
}

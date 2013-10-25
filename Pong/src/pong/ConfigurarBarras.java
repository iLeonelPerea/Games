/**********************************************************************/
/* Autor: Francisco I. Leyva
 * P�gina web: http://www.panchosoft.com
 * Correo electr�nico: yagami_2@hotmail.com
 *
 *
/**********************************************************************/
package pong;
import javax.swing.*;

public class ConfigurarBarras extends javax.swing.JFrame {
    
    /*Creamos los vectores con las rutas a las imagenes.*/
    
    /*Predeterminado.*/
    private int cantidadBarras = 3;
    public ImageIcon[] barras = new ImageIcon[cantidadBarras];
    public ImageIcon[] barraElegida = new ImageIcon[cantidadBarras];
    public ImageIcon barraElegidaMuestra;
    public String[] nombreBarras = new String[cantidadBarras];
    public String[] nombreBarraElegida = new String[cantidadBarras];
    public String nombreBarraElegidaMuestra;
    private Mundo3D padre;

    
    public ConfigurarBarras(Mundo3D mundo) {
        padre = mundo;
        /*Iniciamos variables.*/
        barras[0] = new ImageIcon(getClass().getResource("/pong/images/barraRed.jpg"));
        barras[1] = new ImageIcon(getClass().getResource("/pong/images/barraEstadio.jpg"));
        barras[2] = new ImageIcon(getClass().getResource("/pong/images/barraLava.jpg"));

        nombreBarras[0]="Red";
        nombreBarras[1]="Estadio";
        nombreBarras[2]="Lava";
        
        barraElegidaMuestra = barras[0];
        nombreBarraElegidaMuestra  = nombreBarras[0];

        /*Iniciamos componentes visuales.*/
        initComponents();
    }
    
    // <editor-fold defaultstate="collapsed" desc="Generated Code">//GEN-BEGIN:initComponents
    private void initComponents() {

        lblFicha2 = new javax.swing.JLabel();
        lblInstrucciones = new javax.swing.JLabel();
        cmbBarra = new javax.swing.JComboBox();
        jSeparator1 = new javax.swing.JSeparator();
        jButton1 = new javax.swing.JButton();
        lblInstrucciones1 = new javax.swing.JLabel();
        cmbJugador = new javax.swing.JComboBox();

        setDefaultCloseOperation(javax.swing.WindowConstants.DISPOSE_ON_CLOSE);
        setTitle("Configuraciones");

        lblFicha2.setIcon(new javax.swing.ImageIcon(getClass().getResource("/pong/images/barraRed.jpg"))); // NOI18N

        lblInstrucciones.setIcon(new javax.swing.ImageIcon(getClass().getResource("/pong/images/rating_star.png"))); // NOI18N
        lblInstrucciones.setText("Seleccione el modelo:");

        cmbBarra.setModel(new javax.swing.DefaultComboBoxModel(new String[] { "Red", "Estadio", "Lava" }));
        cmbBarra.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                cmbBarraActionPerformed(evt);
            }
        });

        jButton1.setIcon(new javax.swing.ImageIcon(getClass().getResource("/pong/images/icon_arrow.gif"))); // NOI18N
        jButton1.setText("Aceptar");
        jButton1.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jButton1ActionPerformed(evt);
            }
        });

        lblInstrucciones1.setIcon(new javax.swing.ImageIcon(getClass().getResource("/pong/images/rating_star.png"))); // NOI18N
        lblInstrucciones1.setText("Seleccione el jugador:");

        cmbJugador.setModel(new javax.swing.DefaultComboBoxModel(new String[] { "Jugador 1", "Jugador 2" }));
        cmbJugador.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                cmbJugadorActionPerformed(evt);
            }
        });

        org.jdesktop.layout.GroupLayout layout = new org.jdesktop.layout.GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(org.jdesktop.layout.GroupLayout.LEADING)
            .add(layout.createSequentialGroup()
                .add(layout.createParallelGroup(org.jdesktop.layout.GroupLayout.LEADING)
                    .add(layout.createSequentialGroup()
                        .addContainerGap()
                        .add(jSeparator1, org.jdesktop.layout.GroupLayout.PREFERRED_SIZE, 285, org.jdesktop.layout.GroupLayout.PREFERRED_SIZE))
                    .add(layout.createSequentialGroup()
                        .add(111, 111, 111)
                        .add(jButton1))
                    .add(layout.createSequentialGroup()
                        .add(75, 75, 75)
                        .add(lblFicha2))
                    .add(layout.createSequentialGroup()
                        .add(20, 20, 20)
                        .add(layout.createParallelGroup(org.jdesktop.layout.GroupLayout.TRAILING, false)
                            .add(layout.createSequentialGroup()
                                .add(lblInstrucciones1)
                                .add(19, 19, 19)
                                .add(cmbJugador, org.jdesktop.layout.GroupLayout.PREFERRED_SIZE, 145, org.jdesktop.layout.GroupLayout.PREFERRED_SIZE))
                            .add(layout.createSequentialGroup()
                                .add(lblInstrucciones)
                                .addPreferredGap(org.jdesktop.layout.LayoutStyle.RELATED, org.jdesktop.layout.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                                .add(cmbBarra, org.jdesktop.layout.GroupLayout.PREFERRED_SIZE, 145, org.jdesktop.layout.GroupLayout.PREFERRED_SIZE)))))
                .addContainerGap(org.jdesktop.layout.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(org.jdesktop.layout.GroupLayout.LEADING)
            .add(org.jdesktop.layout.GroupLayout.TRAILING, layout.createSequentialGroup()
                .addContainerGap()
                .add(layout.createParallelGroup(org.jdesktop.layout.GroupLayout.BASELINE)
                    .add(cmbJugador, org.jdesktop.layout.GroupLayout.PREFERRED_SIZE, org.jdesktop.layout.GroupLayout.DEFAULT_SIZE, org.jdesktop.layout.GroupLayout.PREFERRED_SIZE)
                    .add(lblInstrucciones1))
                .addPreferredGap(org.jdesktop.layout.LayoutStyle.RELATED)
                .add(layout.createParallelGroup(org.jdesktop.layout.GroupLayout.BASELINE)
                    .add(cmbBarra, org.jdesktop.layout.GroupLayout.PREFERRED_SIZE, org.jdesktop.layout.GroupLayout.DEFAULT_SIZE, org.jdesktop.layout.GroupLayout.PREFERRED_SIZE)
                    .add(lblInstrucciones))
                .addPreferredGap(org.jdesktop.layout.LayoutStyle.RELATED, org.jdesktop.layout.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                .add(jSeparator1, org.jdesktop.layout.GroupLayout.PREFERRED_SIZE, 10, org.jdesktop.layout.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(org.jdesktop.layout.LayoutStyle.RELATED)
                .add(lblFicha2)
                .addPreferredGap(org.jdesktop.layout.LayoutStyle.RELATED)
                .add(jButton1)
                .addContainerGap())
        );

        pack();
    }// </editor-fold>//GEN-END:initComponents

    private void jButton1ActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jButton1ActionPerformed
        this.dispose();
    }//GEN-LAST:event_jButton1ActionPerformed

    private void cmbBarraActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_cmbBarraActionPerformed
        int indice = cmbBarra.getSelectedIndex();
        if(cmbJugador.getSelectedIndex()==0){
            if(indice==0){
                barraElegidaMuestra = barras[0];
                nombreBarraElegidaMuestra = nombreBarras[0];
                padre.getBarra1().setTextura("images/barra"+nombreBarraElegidaMuestra+".jpg");
            }
            if(indice==1){
                barraElegidaMuestra = barras[1];
                nombreBarraElegidaMuestra = nombreBarras[1];
                padre.getBarra1().setTextura("images/barra"+nombreBarraElegidaMuestra+".jpg");
            }
            if(indice==2){
                barraElegidaMuestra = barras[2];
                nombreBarraElegidaMuestra = nombreBarras[2];
                padre.getBarra1().setTextura("images/barra"+nombreBarraElegidaMuestra+".jpg");
            }
        }
        if(cmbJugador.getSelectedIndex()==1){
            if(indice==0){
                barraElegidaMuestra = barras[0];
                nombreBarraElegidaMuestra = nombreBarras[0];
                padre.getBarra2().setTextura("images/barra"+nombreBarraElegidaMuestra+".jpg");
            }
            if(indice==1){
                barraElegidaMuestra = barras[1];
                nombreBarraElegidaMuestra = nombreBarras[1];
                padre.getBarra2().setTextura("images/barra"+nombreBarraElegidaMuestra+".jpg");
            }
            if(indice==2){
                barraElegidaMuestra = barras[2];
                nombreBarraElegidaMuestra = nombreBarras[2];
                padre.getBarra2().setTextura("images/barra"+nombreBarraElegidaMuestra+".jpg");
            }
        }
        lblFicha2.setIcon( barraElegidaMuestra );
    }//GEN-LAST:event_cmbBarraActionPerformed

    private void cmbJugadorActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_cmbJugadorActionPerformed

    }//GEN-LAST:event_cmbJugadorActionPerformed

    
    // Variables declaration - do not modify//GEN-BEGIN:variables
    private javax.swing.JComboBox cmbBarra;
    private javax.swing.JComboBox cmbJugador;
    private javax.swing.JButton jButton1;
    private javax.swing.JSeparator jSeparator1;
    private javax.swing.JLabel lblFicha2;
    private javax.swing.JLabel lblInstrucciones;
    private javax.swing.JLabel lblInstrucciones1;
    // End of variables declaration//GEN-END:variables
    
}

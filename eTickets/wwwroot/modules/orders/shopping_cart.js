$(function () {
    paypal.Button.render({
        // configure environment
        env: 'sandbox',
        client: {
            sandbox: 'Ad2iqJtLU4D1XP_7RESYdaE6EWjDsymrkiDyG-WRNj1IOj6_XI3TBPvlLSVqQekYkSNWCq3ohK2XeWNs'
        },
        // Customize button
        locale: 'en_US',
        style: {
            size: 'small',
            color: 'gold',
            shape: 'pill'
        },
        commit: true,
        
        //Set up a payment
        payment: function(data, actions) {
            return actions.payment.create({
               transactions: [{
                   amount: {
                       total: _total,
                       currency: 'USD'
                   }
               }] 
            });
        },
        // Execute the payment
        onAuthorize: function(data, actions) {
            return actions.payment.execute().then(function() {
               window.location.href = paymentReturnUrl; 
            });
        }
    }, '#paypal-btn');
});